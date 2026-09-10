'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ContractDescriptionsRepository
    Inherits GenericRepository(Of ContractDescriptions)
    Implements IContractDescriptionsRepository

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
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractDescriptions(code As String) As ContractDescriptions Implements IContractDescriptionsRepository.GetContractDescriptions
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ContractDescriptions In Me._context.ContractDescriptions
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.ContractDescriptions.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New ContractDescriptions()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de descripciones relacionadas por una lista de codigos
    ''' </summary>
    ''' <param name="Listcode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListContractDescriptions(Listcode As List(Of String)) As List(Of ContractDescriptions) Implements IContractDescriptionsRepository.GetListContractDescriptions
        If Listcode Is Nothing OrElse Listcode.Count = 0 Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As ContractDescriptions In Me._context.ContractDescriptions.AsNoTracking()
                   Where Listcode.Contains(d.Code)
                   Select d).ToList()
        If res IsNot Nothing Then

            Return res
        Else
            Return New List(Of ContractDescriptions)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractDescriptionsById(id As Integer) As ContractDescriptions Implements IContractDescriptionsRepository.GetContractDescriptionsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ContractDescriptions Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As ContractDescriptions In Me._context.ContractDescriptions.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New ContractDescriptions()
        End If
    End Function

End Class
