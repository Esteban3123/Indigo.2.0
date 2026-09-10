'***********************************************************************
' Assembly         : Infrastructure.Data.StatementRepository
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 2014-04-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region
Public Class StatementFolioRepository
    Inherits GenericRepository(Of AttachedDeclarations)
    Implements IStatementFolioRepository

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
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub
#End Region

#Region "implements"
    ''' <summary>
    ''' funcion para obtener el folio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFoliobyCode(code As String, Optional tracking As Boolean = True) As AttachedDeclarations Implements IStatementFolioRepository.GetStatementFoliobyCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As AttachedDeclarations In _context.AttachedDeclarations.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As AttachedDeclarations In _context.AttachedDeclarations.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New AttachedDeclarations()
            End If
        Else
            Dim res = (From d As AttachedDeclarations In _context.AttachedDeclarations Where d.Code.Equals(code.Trim()) Select d).ToList()
            If res.Count > 0 Then
                res(0).OriginalValue = (From d As AttachedDeclarations In _context.AttachedDeclarations Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New AttachedDeclarations()
            End If
        End If
    End Function

    ''' <summary>
    ''' funcion para obtene rel folio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFoliobyId(id As Integer, Optional tracking As Boolean = True) As AttachedDeclarations Implements IStatementFolioRepository.GetStatementFoliobyId
        If tracking = False Then
            Dim res = (From d As AttachedDeclarations In _context.AttachedDeclarations.AsNoTracking Where d.Code.Equals(id) Select d).ToList()
            If res IsNot Nothing Then
                Return res(0)
            Else
                Return New AttachedDeclarations()
            End If
        Else
            Dim res = (From d As AttachedDeclarations In _context.AttachedDeclarations Where d.Code.Equals(id) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As AttachedDeclarations In _context.AttachedDeclarations Where d.Code.Equals(id) Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New AttachedDeclarations()
            End If
        End If
    End Function
#End Region
   
End Class
