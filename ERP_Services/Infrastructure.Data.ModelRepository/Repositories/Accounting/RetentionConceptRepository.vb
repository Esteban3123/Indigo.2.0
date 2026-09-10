'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

Public Class RetentionConceptRepository
    Inherits GenericRepository(Of RetentionConcepts)
    Implements IRetentionConceptRepository, Inject


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

#Region "IRetentionConceptRepository"

    ''' <summary>
    ''' Obtiene un concepto de retención
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetRetentionConcept(code As String) As RetentionConcepts Implements IRetentionConceptRepository.GetRetentionConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As RetentionConcepts In Me._context.RetentionConcepts.Include("RetentionConceptRanges").Include("RetentionConceptByCity").Include("RetentionConceptByCity.City") Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res.Count > 0 Then
            res(0).OriginalValue = (From d As RetentionConcepts In Me._context.RetentionConcepts.Include("RetentionConceptRanges").AsNoTracking().Include("RetentionConceptByCity").AsNoTracking().Include("RetentionConceptByCity.City").AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New RetentionConcepts()
        End If
    End Function

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionById(id As Integer, Optional Tracking As Boolean = False) As RetentionConcepts Implements IRetentionConceptRepository.GetRetentionById
        Dim res = New List(Of RetentionConcepts)
        If Tracking Then
            res = (From d As RetentionConcepts In Me._context.RetentionConcepts.AsNoTracking() Where d.Id = id Select d).ToList()
        Else
            res = (From d As RetentionConcepts In Me._context.RetentionConcepts.Include("RetentionConceptRanges").Include("RetentionConceptByCity").Include("RetentionConceptByCity.City") Where d.Id = id Select d).ToList()
        End If
        If res.Count > 0 Then
            res(0).OriginalValue = (From d As RetentionConcepts In Me._context.RetentionConcepts.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New RetentionConcepts()
        End If
    End Function

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity Implements IRetentionConceptRepository.GetRetentionConceptByCity
        Dim retentionConceptByCity As New RetentionConceptByCity
        retentionConceptByCity.Rate = (From rc As RetentionConcepts In Me._context.RetentionConcepts.AsNoTracking() Where rc.Id = id Select rc.Rate).FirstOrDefault()

        Dim city = (From a In Me._context.Address.AsNoTracking() Where a.Id = addressId Select a.CityId).FirstOrDefault()
        If city IsNot Nothing Then
            Dim retentionByCity = (From rc As RetentionConceptByCity In Me._context.RetentionConceptByCity.AsNoTracking() Where rc.RetentionConceptId = id AndAlso rc.CityId = city Select rc).FirstOrDefault()
            If retentionByCity IsNot Nothing Then
                retentionConceptByCity.Rate = retentionByCity.Rate
            End If
        End If

        Return retentionConceptByCity
    End Function

    ''' <summary>
    ''' Obtiene el concepto de retención ICA asociado a una determinada sucursal
    ''' </summary>
    ''' <param name="brachOfficeId">The brach office identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts Implements IRetentionConceptRepository.GetRetentionConceptByIdBrachOfficeId
        Return (From r In _context.RetentionConcepts.AsNoTracking()
                Join c In _context.City.AsNoTracking() On c.ICARetentionConceptId Equals r.Id
                Join b In _context.BranchOffice.AsNoTracking() On b.CityId Equals c.Id
                Where b.Id = brachOfficeId
                Select r).FirstOrDefault()
    End Function

    Public Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts Implements IRetentionConceptRepository.GetIVARetentionConceptByThirdPartyId
        Return (From r In _context.RetentionConcepts.AsNoTracking()
                Join tp In _context.ThirdParty.AsNoTracking() On tp.IVARetentionConceptId Equals r.Id
                Where tp.Id = thirdPartyId
                Select r).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As List(Of RetentionConceptRanges) Implements IRetentionConceptRepository.GetListRetentionRangeByRetentionConceptId
        If RetentionConceptId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Dim res = (From rcr In _context.RetentionConceptRanges.AsNoTracking Where rcr.RetentionId = RetentionConceptId Select rcr).ToList
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
