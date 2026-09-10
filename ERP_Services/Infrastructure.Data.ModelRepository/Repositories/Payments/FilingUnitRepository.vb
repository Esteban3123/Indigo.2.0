'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class FilingUnitRepository
    Inherits GenericRepository(Of FilingUnit)
    Implements IFilingUnitRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub



    ''' <summary>
    ''' Obtiene una unidad de radicacion 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnit(code As String, Optional tracking As Boolean = True) As FilingUnit Implements IFilingUnitRepository.GetFilingUnit
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FilingUnit In Me._context.FilingUnit.Include("FilingUnit1").Include("FilingUnitUser") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParentId IsNot Nothing Then
                Dim filingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = res.ParentId Select fu).FirstOrDefault
                res.FilingUnitDescription = filingUnit.Code + " - " + filingUnit.Name
            End If

            res.OriginalValue = (From d As FilingUnit In Me._context.FilingUnit.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FilingUnit()
        End If
    End Function

    ''' <summary>
    ''' Consulta una unidad de radicacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitById(id As String, Optional tracking As Boolean = True) As FilingUnit Implements IFilingUnitRepository.GetFilingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.FilingUnit Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As FilingUnit In Me._context.FilingUnit.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New FilingUnit()
        End If
    End Function

    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario, tambien carga las unidades funcionales padres
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUser(userCode As String) As List(Of FilingUnit) Implements IFilingUnitRepository.GetFilingUnitByUser
        Dim query = From e In _context.FilingUnitUser.AsNoTracking().Include("FilingUnit").AsNoTracking() Where e.UserCode = userCode Select e.FilingUnit
        Dim listFiling As List(Of FilingUnit) = query.ToList()
        listFiling.ForEach(Sub(item)
                               item.CodeName = item.Code & " - " & item.Name
                               item.IsSon = True
                           End Sub)
        For Each parentId As Nullable(Of Integer) In (From p In listFiling Select New Nullable(Of Integer)(p.ParentId)).ToList()
            RecursiveFilingUnit(parentId, listFiling)
        Next
        Return listFiling
    End Function

    ''' <summary>
    ''' Funcion para traer una lista de unidades de radicacion padres
    ''' </summary>
    ''' <param name="filingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecursiveFilingUnit(filingUnitId As Integer, listFiling As List(Of FilingUnit)) As List(Of FilingUnit)
        If Not listFiling.Exists(Function(x)
                                     If (x.Id = filingUnitId) Then
                                         Return True
                                     Else
                                         Return False
                                     End If
                                 End Function) Then
            Dim query = From e In _context.FilingUnit.AsNoTracking() Where e.Id = filingUnitId Select e
            Dim filing As FilingUnit = query.SingleOrDefault()
            filing.CodeName = filing.Code & " - " & filing.Name
            listFiling.Add(filing)
            If filing.ParentId IsNot Nothing Then
                RecursiveFilingUnit(filing.ParentId, listFiling)
            Else
                Return listFiling
            End If
        Else
            Return listFiling
        End If
        Return listFiling
    End Function


    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByUserPermission(userCode As String) As List(Of FilingUnitUser) Implements IFilingUnitRepository.GetFilingUnitByUserPermission
        Dim query = From e In _context.FilingUnitUser.AsNoTracking().Include("FilingUnit").AsNoTracking() Where e.UserCode = userCode Select e
        Dim listFiling As List(Of FilingUnitUser) = query.ToList()
        Return listFiling
    End Function
End Class
