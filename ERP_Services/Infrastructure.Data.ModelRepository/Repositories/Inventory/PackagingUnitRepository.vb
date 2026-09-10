'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 09-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class PackagingUnitRepository
    Inherits GenericRepository(Of PackagingUnit)
    Implements IPackagingUnitRepository

    ''' <summary>
    ''' Contexto de inventario
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
    ''' obtiene una unidad de paquete por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPackagingUnitByCode(code As String) As PackagingUnit Implements IPackagingUnitRepository.GetPackagingUnitByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PackagingUnit In Me._context.PackagingUnit
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.PackagingUnit.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New PackagingUnit()
        End If
    End Function

    ''' <summary>
    ''' obtiene una unidad de paquete por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPackagingUnitById(id As Integer) As PackagingUnit Implements IPackagingUnitRepository.GetPackagingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PackagingUnit Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As PackagingUnit In Me._context.PackagingUnit.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New PackagingUnit()
        End If
    End Function

End Class
