'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class ConfirmationUnitDoseRepository
    Inherits GenericRepository(Of ConfirmationUnitDose)
    Implements IConfirmationUnitDoseRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetConfirmationUnitDoseById(id As String, Optional tracking As Boolean = True) As ConfirmationUnitDose Implements IConfirmationUnitDoseRepository.GetConfirmationUnitDoseById
        Dim res = (From bg In _context.ConfirmationUnitDose Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ConfirmationUnitDose.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New ConfirmationUnitDose
        End If
    End Function

    ''' <summary>
    ''' Actualiza la orden medica
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_UpdateMedicalOrder(xml As String, userCode As String) As SP_UpdateMedicalOrder_Result Implements IConfirmationUnitDoseRepository.SP_UpdateMedicalOrder
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_UpdateMedicalOrder(xml, userCode).SingleOrDefault
    End Function

End Class