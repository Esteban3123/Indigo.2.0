'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class EarningsTypeRepository
    Inherits GenericRepository(Of RevenueType)
    Implements IEarningsTypeRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetEarningsType(code As String, validityId As Integer, type As Integer, Optional tracking As Boolean = True) As RevenueType Implements IEarningsTypeRepository.GetEarningsType
        Dim earningsType = From e In _context.RevenueType
                     Where e.Code = code AndAlso e.BudgetaryValidityId = validityId AndAlso e.Type = type
                     Select e
        If earningsType.Count > 0 Then
            Dim objearningsType = Nothing
            earningsType.SingleOrDefault().OriginalValue = (From e In _context.RevenueType.AsNoTracking
                                Where e.Code = code AndAlso e.BudgetaryValidityId = validityId AndAlso e.Type = type
                                Select e).SingleOrDefault
            objearningsType = earningsType.SingleOrDefault()
            Return objearningsType
        Else
            Return New RevenueType()
        End If
    End Function

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    Public Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType) Implements IEarningsTypeRepository.ListEarningsTypeByValidity
        Dim search As List(Of RevenueType) = (From e In _context.RevenueType Select e).ToList()
        Return search
    End Function

    ''' <summary>
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetEarningsTypeByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As RevenueType Implements IEarningsTypeRepository.GetEarningsTypeByValidity
        'Dim earningsType = From e In _context.RevenueType.Include("BudgetaryValidity")
        '             Where e.Code = code And e.ValidityId = ValidityId
        '             Select e
        'If earningsType.Count > 0 Then
        '    Dim objearningsType = Nothing
        '    earningsType.SingleOrDefault().OriginalValue = (From e In _context.RevenueType.AsNoTracking
        '                        Where e.Code = code And e.ValidityId = ValidityId
        '                        Select e).SingleOrDefault
        '    objearningsType = earningsType.SingleOrDefault()
        '    Return objearningsType
        'Else
        '    Return New RevenueType()
        'End If
        Return New RevenueType()
    End Function

    ''' <summary>
    ''' Obtiene todas los tipos de ingreso=1 o tipos de gasto=2 para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRevenueTypeForCopyBase(validityId As Integer, type As Integer) As List(Of RevenueType) Implements IEarningsTypeRepository.GetListRevenueTypeForCopyBase
        Dim listRevenueType = (From e In _context.RevenueType.AsNoTracking Where e.BudgetaryValidityId = validityId AndAlso e.Type = type Select e).ToList
        If listRevenueType.Count > 0 Then
            Return listRevenueType
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el tipo de ingreso=1 o tipo de gasto=2 por codigo, vigencia y tipo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRevenueTypeByCodeAndValidityForCopyBase(code As String, validityId As Integer, type As Integer) As RevenueType Implements IEarningsTypeRepository.GetRevenueTypeByCodeAndValidityForCopyBase
        Return (From e In _context.RevenueType.AsNoTracking Where e.BudgetaryValidityId = validityId AndAlso e.Code = code AndAlso e.Type = type Select e).FirstOrDefault
    End Function

#End Region

End Class
