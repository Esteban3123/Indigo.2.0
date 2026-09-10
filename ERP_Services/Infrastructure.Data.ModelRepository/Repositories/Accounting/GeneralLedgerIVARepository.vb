'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 06-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region


''' <summary>
''' Repositorio de la entidad GeneralLedgerIVA
''' </summary>
Public Class GeneralLedgerIVARepository
    Inherits GenericRepository(Of GeneralLedgerIVA)
    Implements IGeneralLedgerIVARepository

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="context">Contexto de contabilidad</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        Me._context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' <see cref="Domain.Entities.IGeneralLedgerIVARepository.GetGeneralLedgerIVAByCode" />
    ''' </summary>
    ''' <param name="code"><see cref="Domain.Entities.IGeneralLedgerIVARepository.GetGeneralLedgerIVAByCode" /></param>
    ''' <returns><see cref="Domain.Entities.IDocumentTypeRepository.GetDocumentType" /></returns>
    Public Function GetGeneralLedgerIVAByCode(code As String) As GeneralLedgerIVA Implements IGeneralLedgerIVARepository.GetGeneralLedgerIVAByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As GeneralLedgerIVA In Me._context.GeneralLedgerIVA Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res.Count() > 0 Then
            res(0).OriginalValue = (From d As GeneralLedgerIVA In Me._context.GeneralLedgerIVA.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New GeneralLedgerIVA()
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener el tipo de documento  contable por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetGeneralLedgerIVAById(id As Integer) As GeneralLedgerIVA Implements IGeneralLedgerIVARepository.GetGeneralLedgerIVAById
        Dim query = From e In _context.GeneralLedgerIVA.AsNoTracking()
                     Where e.Id = id
                     Select e

        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

#End Region

    'Public Function GetGeneralLedgerIVAByCode(code As String) As GeneralLedgerIVA Implements IGeneralLedgerIVARepository.GetGeneralLedgerIVAByCode
    '    Dim res = (From gli In _context.GeneralLedgerIVA Where gli.Code = code Select gli).FirstOrDefault()
    '    If res IsNot Nothing Then
    '        res.OriginalValue = (From gli In _context.GeneralLedgerIVA.AsNoTracking() Where gli.Code = code Select gli).FirstOrDefault()
    '        Return res
    '    Else
    '        Return New GeneralLedgerIVA
    '    End If
    'End Function

    'Public Function GetGeneralLedgerIVAById(id As Integer) As GeneralLedgerIVA Implements IGeneralLedgerIVARepository.GetGeneralLedgerIVAById
    '    Dim res = (From gli In _context.GeneralLedgerIVA Where gli.Id = id Select gli).FirstOrDefault()
    '    If res IsNot Nothing Then
    '        Return res
    '    Else
    '        Return New GeneralLedgerIVA
    '    End If
    'End Function
End Class
