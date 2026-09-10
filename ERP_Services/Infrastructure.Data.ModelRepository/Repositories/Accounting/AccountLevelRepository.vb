'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingLevelRepository
' Author           : Sergio Fernandez
' Created          : 2014-12-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region
Public Class AccountLevelRepository
    Inherits GenericRepository(Of MainAccountLevels)
    Implements IAccountLevelRepository


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

#Region "Implements"

    ''' <summary>
    ''' Gets the account Level by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelByCode(Code As String, Optional tracking As Boolean = True) As MainAccountLevels Implements IAccountLevelRepository.GetAccountLevelByCode
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the account Level by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelById(id As Integer, Optional tracking As Boolean = True) As MainAccountLevels Implements IAccountLevelRepository.GetAccountLevelById
        If tracking = False Then
            Dim res = (From d As MainAccountLevels In _context.MainAccountLevels.AsNoTracking Where d.Id.Equals(id) Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As MainAccountLevels In _context.MainAccountLevels.AsNoTracking Where d.Id = id Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New MainAccountLevels()
            End If
        Else
            Dim res = (From d As MainAccountLevels In _context.MainAccountLevels Where d.Id = id Select d).ToList()
            If res IsNot Nothing Then
                res(0).OriginalValue = (From d As MainAccountLevels In _context.MainAccountLevels Where d.Id = id Select d).SingleOrDefault()
                Return res(0)
            Else
                Return New MainAccountLevels()
            End If
        End If
    End Function

    ''' <summary>
    ''' Gets all acount level.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAcountLevel() As List(Of MainAccountLevels) Implements IAccountLevelRepository.GetAllAcountLevel
        Dim query = From e In _context.MainAccountLevels
                    Select e
        If query.Count > 0 Then
            Return query.ToList()
        Else
            Return Nothing
        End If
    End Function


#End Region

 
End Class
