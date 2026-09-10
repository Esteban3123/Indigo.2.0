'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ContractMinimumWageRepository
    Inherits GenericRepository(Of ContractMinimumWage)
    Implements IContractMinimumWageRepository

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
    ''' Obtiene un salirio minimo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetContractMinimumWage(code As String) As ContractMinimumWage Implements IContractMinimumWageRepository.GetContractMinimumWage
        Dim res = (From cmw In _context.ContractMinimumWage Where cmw.Code = code Select cmw).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From cmw In _context.ContractMinimumWage.AsNoTracking() Where cmw.Code = code Select cmw).FirstOrDefault()
            Return res
        Else
            Return New ContractMinimumWage
        End If
    End Function

    ''' <summary>
    ''' Obtiene un salrio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetContractMinimumWageById(id As Integer) As ContractMinimumWage Implements IContractMinimumWageRepository.GetContractMinimumWageById
        Dim res = (From cmw In _context.ContractMinimumWage Where cmw.Id = id Select cmw).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From cmw In _context.ContractMinimumWage Where cmw.Id = id Select cmw).FirstOrDefault()
            Return res
        Else
            Return New ContractMinimumWage
        End If
    End Function
End Class
