'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class BudgetItemRepository
    Inherits GenericRepository(Of Category)
    Implements IBudgetItemRepository

    'Coexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    ''' <param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Public Function GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category Implements IBudgetItemRepository.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType
        Dim result As Category = Nothing

        If FinancialSourceId Is Nothing Then
            result = (From e In _context.Category
                      Where e.FinancialSourceId Is Nothing AndAlso e.Code = Code AndAlso e.BudgetaryValidityId = BudgetaryValidityId AndAlso e.ItemType = ItemType
                      Select e).FirstOrDefault
        Else
            result = (From e In _context.Category
                      Where e.FinancialSourceId = FinancialSourceId AndAlso e.Code = Code AndAlso e.BudgetaryValidityId = BudgetaryValidityId AndAlso e.ItemType = ItemType
                      Select e).FirstOrDefault
        End If

        If result IsNot Nothing Then
            If result.FinancialSourceId IsNot Nothing Then
                Dim financialSource = (From f In _context.FinancialSource.AsNoTracking Where f.Id = result.FinancialSourceId Select f).FirstOrDefault
                result.FinancialSourceDescription = financialSource.Code + " - " + financialSource.Name
            End If

            If result.CategoryOwnerId IsNot Nothing Then
                Dim parent = (From p In _context.Category.AsNoTracking Where p.Id = result.CategoryOwnerId Select p).FirstOrDefault
                result.ParentDescription = parent.Code + " - " + parent.Name
            End If

            If result.CCPETCodeId IsNot Nothing Then
                Dim DescCCPET = (From p In _context.CCPET.AsNoTracking Where p.Id = result.CCPETCodeId Select p).FirstOrDefault
                result.CCPETDescription = DescCCPET.Code + " - " + DescCCPET.Name
            End If
            If result.CPCCodeId IsNot Nothing Then
                Dim DescCPC = (From p In _context.CPCCatalog.AsNoTracking Where p.Id = result.CPCCodeId Select p).FirstOrDefault
                result.CPCDescription = DescCPC.Code + " - " + DescCPC.Name
            End If
            If result.PublicPolicyId IsNot Nothing Then
                Dim DescPP = (From p In _context.PublicPolicy.AsNoTracking Where p.Id = result.PublicPolicyId Select p).FirstOrDefault
                result.PublicPolicyDescription = DescPP.Code + " - " + DescPP.Name
            End If
            result.OriginalValue = (From e In _context.Category.AsNoTracking Where e.Id = result.Id Select e).FirstOrDefault

            Return result
        Else
            Return New Category
        End If
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    ''' <param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Public Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category Implements IBudgetItemRepository.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType
        Dim result = (From e In _context.Category
                      Where e.Code = Code And e.BudgetaryValidityId = BudgetaryValidityId And e.ItemType = ItemType
                      Select e).FirstOrDefault

        If result IsNot Nothing Then

            If result.FinancialSourceId IsNot Nothing Then
                Dim financialSource = (From f In _context.FinancialSource.AsNoTracking Where f.Id = result.FinancialSourceId Select f).FirstOrDefault
                result.FinancialSourceDescription = financialSource.Code + " - " + financialSource.Name
            End If

            If result.CategoryOwnerId IsNot Nothing Then
                Dim parent = (From p In _context.Category.AsNoTracking Where p.Id = result.CategoryOwnerId Select p).FirstOrDefault
                result.ParentDescription = parent.Code + " - " + parent.Name
            End If

            result.OriginalValue = (From e In _context.Category.AsNoTracking
                                    Where e.Code = Code And e.BudgetaryValidityId = BudgetaryValidityId And e.ItemType = ItemType
                                    Select e).FirstOrDefault

            Return result
        Else
            Return New Category
        End If
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    ''' <param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Public Function GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category Implements IBudgetItemRepository.GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType
        Dim result = (From e In _context.Category.AsNoTracking().Include("FinancialSource").AsNoTracking
                      Where e.Code = Code And e.BudgetaryValidityId = BudgetaryValidityId And e.ItemType = ItemType
                      Select e).FirstOrDefault
        If result IsNot Nothing Then
            Return result
        Else
            Return New Category
        End If
    End Function

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetItemsByValidity(ValidityId As String, ItemType As Byte) As List(Of Category) Implements IBudgetItemRepository.ListBudgetItemsByValidity
        Dim result = From e In _context.Category.Include("FinancialSource").Include("LevelCategory")
                     Where e.BudgetaryValidityId = ValidityId And e.ItemType = ItemType
                     Select e

        If result.Count > 0 Then
            Return result.ToList
        Else
            Return New List(Of Category)()
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener el listado de
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function GetAllBudgetItemsByState(state As Boolean) As List(Of Category) Implements IBudgetItemRepository.GetAllBudgetItemsByState
        Dim query = From e In _context.Category
                    Where e.Status = state
                    Select e
        If (query.Count > 0) Then
            Return query.ToList()
        End If
        Return Nothing
    End Function

    Public Function CountCategories() As Integer Implements IBudgetItemRepository.CountCategories
        Dim query = (From e In _context.Category).Count
        Return query
    End Function

    ''' <summary>
    ''' Consulta un rubro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCategoryById(Id As Integer) As Category Implements IBudgetItemRepository.GetCategoryById
        Return (From c In _context.Category.AsNoTracking() Where c.Id = Id Select c).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene todos los rubros padre de ingreso=1 o rubros padre de gasto=2 para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListCategoryForCopyBase(validityId As Integer, type As Integer) As List(Of Category) Implements IBudgetItemRepository.GetListCategoryForCopyBase
        Dim listCategory = From e In _context.Category.AsNoTracking.
                               Include("FinancialSource").AsNoTracking
                           Where e.BudgetaryValidityId = validityId AndAlso e.ItemType = type
                           Select e
        If listCategory.Count > 0 Then
            Return listCategory.ToList
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el rubro de ingreso=1 o rubro de gasto=2 por vigencia, tipo (Ingreso, gasto), codigo y codigo de la fuente de financiación
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="type"></param>
    ''' <param name="code"></param>
    ''' <param name="financialSourceCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCategoryByCodeAndValidityForCopyBase(validityId As Integer, type As Integer, code As String, financialSourceCode As String) As Category Implements IBudgetItemRepository.GetCategoryByCodeAndValidityForCopyBase
        If financialSourceCode Is Nothing Then
            Return (From e In _context.Category.AsNoTracking.
                        Include("FinancialSource").AsNoTracking
                    Where e.BudgetaryValidityId = validityId AndAlso e.ItemType = type AndAlso
                        e.Code = code AndAlso e.FinancialSource Is Nothing
                    Select e).FirstOrDefault
        Else
            Return (From e In _context.Category.AsNoTracking.
                    Include("FinancialSource").AsNoTracking
                    Where e.BudgetaryValidityId = validityId AndAlso e.ItemType = type AndAlso
                        e.Code = code AndAlso e.FinancialSource IsNot Nothing AndAlso e.FinancialSource.Code = financialSourceCode
                    Select e).FirstOrDefault
        End If

    End Function

    ''' <summary>
    ''' Actualizar parametros de presupuesto
    ''' </summary>
    ''' <param name="XmlCriterias"></param>
    ''' <returns></returns>
    Public Function SP_UpdateParameterizedInformation(XmlCriterias As String) As List(Of SP_UpdateParameterizedInformation_Result) Implements IBudgetItemRepository.SP_UpdateParameterizedInformation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_UpdateParameterizedInformation(XmlCriterias).ToList()
    End Function

#End Region

End Class
