'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetDependencyRepository
    Inherits GenericRepository(Of Dependency)
    Implements IBudgetDependencyRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetDependency(code As String, validityId As Integer, Optional tracking As Boolean = True) As Dependency Implements IBudgetDependencyRepository.GetBudgetDependency
        Dim Dependency = (From e In _context.Dependency
                     Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                     Select e).FirstOrDefault
        If Dependency IsNot Nothing Then
            Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = Dependency.ResponsibleId Select t).FirstOrDefault
            Dependency.ResponsibleDescription = thirdParty.Nit + " - " + thirdParty.Name

            Dependency.OriginalValue = (From e In _context.Dependency.AsNoTracking
                                Where e.Code = code AndAlso e.BudgetaryValidityId = validityId
                                Select e).FirstOrDefault
            Return Dependency
        Else
            Return New Dependency()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetDependencyByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As Dependency Implements IBudgetDependencyRepository.GetBudgetDependencyByValidity
        'Dim Dependency = From e In _context.Dependency.Include("BudgetaryValidity")
        '             Where e.Code = code And e.ValidityId = ValidityId
        '             Select e
        'If Dependency.Count > 0 Then
        '    Dim objBudgetDependency = Nothing
        '    Dependency.SingleOrDefault().OriginalValue = (From e In _context.Dependency.AsNoTracking
        '                        Where e.Code = code And e.ValidityId = ValidityId
        '                        Select e).SingleOrDefault
        '    objBudgetDependency = Dependency.SingleOrDefault()
        '    Return objBudgetDependency
        'Else
        '    Return New Dependency()
        'End If
        Return New Dependency()
    End Function

    ''' <summary>
    ''' Obtiene una dependencia por Id
    ''' </summary>
    ''' <param name="id">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetDependencyById(id As Integer, Optional tracking As Boolean = True) As Dependency Implements IBudgetDependencyRepository.GetBudgetDependencyById
        Dim Dependency = (From e In _context.Dependency
                     Where e.Id = id
                     Select e).FirstOrDefault
        If Dependency IsNot Nothing Then
            Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = Dependency.ResponsibleId Select t).FirstOrDefault
            Dependency.ResponsibleDescription = thirdParty.Nit + " - " + thirdParty.Name

            Dependency.OriginalValue = (From e In _context.Dependency.AsNoTracking
                                Where e.Id = id
                                Select e).FirstOrDefault
            Return Dependency
        Else
            Return New Dependency()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todas las dependencias para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListDependencyForCopyBase(validityId As Integer) As List(Of Dependency) Implements IBudgetDependencyRepository.GetListDependencyForCopyBase
        Dim listDependency = (From e In _context.Dependency.AsNoTracking Where e.BudgetaryValidityId = validityId Select e).ToList
        If listDependency.Count > 0 Then
            Return listDependency
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la dependencia por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDependencyByCodeAndValidityForCopyBase(code As String, validityId As Integer) As Dependency Implements IBudgetDependencyRepository.GetDependencyByCodeAndValidityForCopyBase
        Return (From e In _context.Dependency.AsNoTracking Where e.BudgetaryValidityId = validityId AndAlso e.Code = code Select e).FirstOrDefault
    End Function

#End Region

End Class

