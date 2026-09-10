'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CheckRepository
    Inherits GenericRepository(Of Checkbooks)
    Implements ICheckRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una chequeda por id de cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdEntity</exception>
    Public Function GetCheckByIdEntityBankAccountAndStatus(IdEntity As Integer, status As Short) As Checkbooks Implements ICheckRepository.GetCheckByIdEntityBankAccountAndStatus
        If IdEntity = 0 Then
            Throw New ArgumentNullException("IdEntity")
        End If
        Dim res = (From d As Checkbooks In Me._context.Checkbooks.Include("CheckBlock") Where d.IdEntityBanckAccount.Equals(IdEntity) And d.Status = CByte(status) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As Checkbooks In Me._context.Checkbooks.Include("CheckBlock").AsNoTracking() Where d.IdEntityBanckAccount.Equals(IdEntity) And d.Status = CByte(status) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New Checkbooks()
        End If
    End Function

    'Public Function GetCheckByIdEntityBankAccountAndStatus(IdEntity As Integer, status As Short) As Checkbooks Implements ICheckRepository.GetCheckByIdEntityBankAccountAndStatus
    '    If IdEntity = 0 Then
    '        Throw New ArgumentNullException("IdEntity")
    '    End If
    '    Dim state As Byte = CByte(status)
    '    Dim res = (From d As Checkbooks In Me._context.Checkbooks.Include("CheckBlock").Include("OutstandingChecks").AsNoTracking() Where d.IdEntityBanckAccount = IdEntity And d.Status = state Select d).FirstOrDefault()
    '    If res IsNot Nothing AndAlso res.Id > 0 Then
    '        res.OriginalValue = (From d As Checkbooks In Me._context.Checkbooks.AsNoTracking() Where d.IdEntityBanckAccount.Equals(IdEntity) And d.Status = CByte(status) Select d).FirstOrDefault()
    '        Return res
    '    Else
    '        Return New Checkbooks()
    '    End If
    'End Function

    Public Function SP_SaveCheckNumber(operatingUnitId As Integer, EntitybankAccountId As Integer, checkNumber As Long, userId As Integer) As SP_SaveCheckNumber_Result Implements ICheckRepository.SP_SaveCheckNumber
        Return _context.SP_SaveCheckNumber(operatingUnitId, EntitybankAccountId, checkNumber, userId).FirstOrDefault()
    End Function

End Class
