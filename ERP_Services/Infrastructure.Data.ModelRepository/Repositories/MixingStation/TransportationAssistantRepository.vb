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

Public Class TransportationAssistantRepository
    Inherits GenericRepository(Of TransportationAssistant)
    Implements ITransportationAssistantRepository, Inject

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

    Public Function GetTransportationAssistant(code As String, Optional tracking As Boolean = True) As TransportationAssistant Implements ITransportationAssistantRepository.GetTransportationAssistant
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim TransportationAssistant As TransportationAssistant = Nothing

        If tracking Then
            TransportationAssistant = (From e In _context.TransportationAssistant
                                       Where e.Code = code
                                       Select e).FirstOrDefault()
        Else
            TransportationAssistant = (From e In _context.TransportationAssistant.AsNoTracking()
                                       Where e.Code = code
                                       Select e).FirstOrDefault()
        End If

        If TransportationAssistant IsNot Nothing Then
            If TransportationAssistant.PositionId IsNot Nothing Then
                TransportationAssistant.PositionDescription = (From x In _context.Position.AsNoTracking Where x.Id = TransportationAssistant.PositionId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If TransportationAssistant.EmployeeId IsNot Nothing Then
                TransportationAssistant.EmployeeDescription = (From e In _context.Employee.AsNoTracking
                                                               Join t In _context.ThirdParty.AsNoTracking On t.Id Equals e.ThirdPartyId
                                                               Where e.Id = TransportationAssistant.EmployeeId
                                                               Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            End If

            Return TransportationAssistant
        Else
            Return New TransportationAssistant()
        End If
    End Function

    Public Function GetTransportationAssistantById(id As String, Optional tracking As Boolean = True) As TransportationAssistant Implements ITransportationAssistantRepository.GetTransportationAssistantById
        Dim res = (From bg In _context.TransportationAssistant Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.TransportationAssistant.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New TransportationAssistant
        End If
    End Function

End Class