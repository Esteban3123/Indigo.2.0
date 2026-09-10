'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Sergio Fernandez
' Created          : 2014-10-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class AccountClassRepository
    Inherits GenericRepository(Of MainAccountClasses)
    Implements IAccountClassRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Gets the account class by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountClassByCode(Code As String, Optional tracking As Boolean = True) As MainAccountClasses Implements IAccountClassRepository.GetAccountClassByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If


        Dim res = (From d As MainAccountClasses In Me._context.MainAccountClasses Where d.Code.Equals(Code.Trim()) Select d).ToList()
        If res.Count() > 0 Then
            res(0).OriginalValue = (From d As MainAccountClasses In Me._context.MainAccountClasses.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New MainAccountClasses()
        End If
    End Function

    ''' <summary>
    ''' Gets the account class by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountClassById(id As Integer, Optional tracking As Boolean = True) As MainAccountClasses Implements IAccountClassRepository.GetAccountClassById
        If tracking = False Then
            Dim res = (From d As MainAccountClasses In _context.MainAccountClasses.AsNoTracking Where d.Id = id Select d).ToList()
            If res.Count() > 0 Then
                res(0).OriginalValue = (From d As MainAccountClasses In _context.MainAccountClasses.AsNoTracking Where d.Id = id Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New MainAccountClasses()
            End If
        Else
            Dim res = (From d As MainAccountClasses In _context.MainAccountClasses Where d.Id = id Select d).ToList()
            If res.Count() > 0 Then
                res(0).OriginalValue = (From d As MainAccountClasses In _context.MainAccountClasses Where d.Id = id Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New MainAccountClasses()
            End If
        End If
    End Function

    ''' <summary>
    ''' Gets all account class.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountClass() As List(Of MainAccountClasses) Implements IAccountClassRepository.GetAllAccountClass
        Dim query = From e In _context.MainAccountClasses
                     Select e
        If query.Count > 0 Then
            Return query.ToList
        Else
            Return Nothing
        End If
    End Function
#End Region

End Class
