'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/08/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetEntryDevolutionRepository
    Inherits GenericRepository(Of FixedAssetEntryDevolution)
    Implements IFixedAssetEntryDevolutionRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryDevolution(code As String) As FixedAssetEntryDevolution Implements IFixedAssetEntryDevolutionRepository.GetFixedAssetEntryDevolution
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetEntryDevolution.Include("FixedAssetEntryDevolutionObligationBudget") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim entry = (From e In _context.FixedAssetEntry.AsNoTracking.Include("Supplier").AsNoTracking Where e.Id = res.FixedAssetEntryId Select e).FirstOrDefault
            res.EntryDescription = entry.Code + " - " + entry.Supplier.Code + " - " + entry.Supplier.Name

            If res.FixedAssetEntryDevolutionObligationBudget IsNot Nothing AndAlso res.FixedAssetEntryDevolutionObligationBudget.Count > 0 Then
                For Each item In res.FixedAssetEntryDevolutionObligationBudget
                    Dim obligationDetail = (From x In _context.ObligationDetail.AsNoTracking().Include("Obligation").AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking
                                            Where x.Id = item.ObligationDetailId
                                            Select x).FirstOrDefault()
                    item.ObligationCode = obligationDetail.Obligation.Code
                    item.ObligationDocument = obligationDetail.Obligation.Document
                    item.CategoryName = obligationDetail.Category.Code + " - " + obligationDetail.Category.Name
                    If obligationDetail.Category.FinancialSource IsNot Nothing Then
                        item.FinancialSourceDescription = obligationDetail.Category.FinancialSource.Code + " - " + obligationDetail.Category.FinancialSource.Name
                    End If
                    item.RevenueTypeDescription = obligationDetail.RevenueType.Code + " - " + obligationDetail.RevenueType.Name
                    item.ObligationBalance = obligationDetail.Balance
                    item.CommitmentDetailId = obligationDetail.CommitmentDetailId
                Next
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetEntryDevolution.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetEntryDevolution()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryDevolutionById(Id As Integer) As FixedAssetEntryDevolution Implements IFixedAssetEntryDevolutionRepository.GetFixedAssetEntryDevolutionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetEntryDevolution.AsNoTracking.Include("FixedAssetEntryDevolutionDetail").AsNoTracking Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New FixedAssetEntryDevolution()
        End If
    End Function

    ''' <summary>
    ''' Confirma la salida de activos
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveFixedAssetEntryDevolution(XmlObject As String, codeUser As String) As SP_SaveFixedAssetDevolution_Result Implements IFixedAssetEntryDevolutionRepository.SP_SaveFixedAssetEntryDevolution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveFixedAssetDevolution(XmlObject, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene un ingreso de activos por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryById(Id As Integer) As FixedAssetEntry Implements IFixedAssetEntryDevolutionRepository.GetFixedAssetEntryById
        Return (From e In _context.FixedAssetEntry.AsNoTracking Where e.Id = Id).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene la cuenta por pagar asociada al ingreso de activos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableById(Id As Integer) As List(Of AccountPayable) Implements IFixedAssetEntryDevolutionRepository.GetAccountPayableById
        Dim accountPayable = (From a In _context.AccountPayable.AsNoTracking.Include("AccountPayableDetailConcept").AsNoTracking.Include("AccountPayableShares").AsNoTracking Where a.Id = Id Select a).FirstOrDefault
        Dim ListAccountPayable As New List(Of AccountPayable)
        If accountPayable IsNot Nothing Then
            ListAccountPayable.Add(accountPayable)
            Return ListAccountPayable
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el articulo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryItemDetailById(Id As Integer) As FixedAssetEntryItemDetail Implements IFixedAssetEntryDevolutionRepository.GetFixedAssetEntryItemDetailById
        Return (From x In _context.FixedAssetEntryItemDetail.AsNoTracking.Include("FixedAssetEntryItem").AsNoTracking.Include("FixedAssetEntryItem.FixedAssetItem").AsNoTracking
                 Where x.Id = Id
                 Select x).FirstOrDefault
    End Function



    ''' <summary>
    '''  Obtiene una lista de elementos de entrada de activo fijo (FixedAssetEntryItem) asociados a una devolución específica.
    '''  se obtiene solo elementos únicos (sin duplicados).
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetEntryItemsForDevolution(Id As Integer) As List(Of FixedAssetEntryItem) Implements IFixedAssetEntryDevolutionRepository.GetFixedAssetEntryItemsForDevolution
        Return (From x In _context.FixedAssetEntryDevolutionDetail.AsNoTracking().Include("FixedAssetEntryItem").AsNoTracking()
                Where x.FixedAssetEntryDevolutionId = Id
                Select x.FixedAssetEntryItem).Distinct().ToList()
    End Function

End Class
