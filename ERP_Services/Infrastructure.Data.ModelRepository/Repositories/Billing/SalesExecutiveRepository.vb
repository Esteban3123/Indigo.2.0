'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Andres Alarcon
' Created          : 10-04-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SalesExecutiveRepository
    Inherits GenericRepository(Of SalesExecutive)
    Implements ISalesExecutiveRepository

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
    ''' Obtiene un ejecutivo de ventas por codigo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalesExecutiveById(Id As Integer) As SalesExecutive Implements ISalesExecutiveRepository.GetSalesExecutiveById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Dim Res = (From d As SalesExecutive In Me._context.SalesExecutive.AsNoTracking()
                   Where d.Id = Id
                   Select d).FirstOrDefault

        If Res IsNot Nothing Then
            Return Res
        Else
            Return New SalesExecutive()
        End If

    End Function

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por codigo
    ''' </summary>
    ''' <param name="code">codigo de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalesExecutiveByCode(code As String) As SalesExecutive Implements ISalesExecutiveRepository.GetSalesExecutiveByCode
        If code Is Nothing OrElse code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If

        Dim res = (From d As SalesExecutive In Me._context.SalesExecutive
                   Where d.Code = code
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim ThirdParty = (From x In _context.ThirdParty.AsNoTracking Where x.Id = res.ThirdPartyId Select New With {.Nit = x.Nit, .Name = x.Name}).FirstOrDefault
            res.ThirdPartyNitName = String.Concat(ThirdParty.Nit, " - ", ThirdParty.Name)

            Return res
        Else
            Return New SalesExecutive()
        End If
    End Function

End Class
