'***********************************************************************
' Assembly         : Infrastructure.Data.AuthorizationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class PatientExternalCareCenterRepository
    Inherits GenericRepository(Of PatientExternalCareCenter)
    Implements IPatientExternalCareCenterRepository, Inject

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

    Public Function GetPatientExternalCareCenterByCode(code As String) As PatientExternalCareCenter Implements IPatientExternalCareCenterRepository.GetPatientExternalCareCenterByCode
        Dim res = (From bg In _context.PatientExternalCareCenter Where bg.IdentificationNumber = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.PatientExternalCareCenter.AsNoTracking() Where bg.IdentificationNumber = code Select bg).FirstOrDefault()
            res.DescriptionIdentificationType = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking Where x.ID = res.IdentificationTypeId Select String.Concat(x.CODIGO, " - ", x.NOMBRE)).FirstOrDefault
            Return res
        Else
            Return New PatientExternalCareCenter
        End If
    End Function

    Public Function GetPatientExternalCareCenterById(id As Integer) As PatientExternalCareCenter Implements IPatientExternalCareCenterRepository.GetPatientExternalCareCenterById
        Dim res = (From bg In _context.PatientExternalCareCenter Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New PatientExternalCareCenter
        End If
    End Function

    ''' <summary>
    '''  Guarda un convenio
    ''' </summary>
    ''' <param name="EntityXml"></param>    
    ''' <returns></returns>
    Public Function SP_SavePatientExternalCareCenter(EntityXml As String, userCode As String) As SP_SavePatientExternalCareCenter_Result Implements IPatientExternalCareCenterRepository.SP_SavePatientExternalCareCenter
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePatientExternalCareCenter(EntityXml, userCode).SingleOrDefault
    End Function
End Class
