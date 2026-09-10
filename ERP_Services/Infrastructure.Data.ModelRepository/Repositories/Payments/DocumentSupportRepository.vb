'************************************************************
' Assembly         : Infrastructure.Data.Payments
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 2021-01-14
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class DocumentSupportRepository
    Inherits GenericRepository(Of DocumentSupport)
    Implements IDocumentSupportRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListDocumentSupportByUserCode(userCode As String) As List(Of DocumentSupport) Implements IDocumentSupportRepository.ListDocumentSupportByUserCode
        Dim res = (From a As DocumentSupport
                   In Me._context.DocumentSupport.Include("DocumentSupportUser")
                   Where a.Status = True And a.DocumentSupportUser.Any(Function(u) u.UserCode.Equals(userCode))
                   Select a).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of DocumentSupport)()
        End If
    End Function

    ''' <summary>
    ''' Gets the billing authorization by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetDocumentSupportById(Id As Integer, Optional tracking As Boolean = True) As DocumentSupport Implements IDocumentSupportRepository.GetDocumentSupportById
        Dim res As DocumentSupport
        If tracking = True Then
            res = (From a As DocumentSupport
                   In Me._context.DocumentSupport
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As DocumentSupport
                   In Me._context.DocumentSupport.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.DocumentSupport.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New DocumentSupport
        End If
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportByCode(code As String) As DocumentSupport Implements IDocumentSupportRepository.GetDocumentSupportByCode
        Dim res = (From ba In _context.DocumentSupport.Include("DocumentSupportUser") Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From ba In _context.DocumentSupport.AsNoTracking.Include("DocumentSupportUser").AsNoTracking
                                 Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault

            Return res
        Else
            Return New DocumentSupport()
        End If

    End Function

End Class
