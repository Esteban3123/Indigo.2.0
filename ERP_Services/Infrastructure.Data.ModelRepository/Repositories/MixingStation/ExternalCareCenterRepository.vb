'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class ExternalCareCenterRepository
    Inherits GenericRepository(Of ExternalCareCenter)
    Implements IExternalCareCenterRepository, Inject

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

    Public Function GetExternalCareCenter(code As String, Optional tracking As Boolean = True) As ExternalCareCenter Implements IExternalCareCenterRepository.GetExternalCareCenter
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim ExternalCareCenter As ExternalCareCenter = Nothing

        If tracking Then
            ExternalCareCenter = (From e In _context.ExternalCareCenter.Include("ExternalCareCenterUsers")
                                  Where e.Code = code
                                  Select e).FirstOrDefault()
        Else
            ExternalCareCenter = (From e In _context.ExternalCareCenter.AsNoTracking().Include("ExternalCareCenterUsers").AsNoTracking
                                  Where e.Code = code
                                  Select e).FirstOrDefault()
        End If

        If ExternalCareCenter IsNot Nothing Then
            ExternalCareCenter.CustomerDescription = (From t In _context.Customer.AsNoTracking Where t.Id = ExternalCareCenter.CustomerId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            ExternalCareCenter.ContractExternalClientsDescription = (From t In _context.ContractExternalClients.AsNoTracking Where t.Id = ExternalCareCenter.ContractExternalClientsId Select String.Concat(t.Code, " - ", t.ContractNumber)).FirstOrDefault()

            Return ExternalCareCenter
        Else
            Return New ExternalCareCenter()
        End If
    End Function

    Public Function GetExternalCareCenterById(id As String, Optional tracking As Boolean = True) As ExternalCareCenter Implements IExternalCareCenterRepository.GetExternalCareCenterById
        Dim res = (From bg In _context.ExternalCareCenter Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ExternalCareCenter.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New ExternalCareCenter
        End If
    End Function
End Class