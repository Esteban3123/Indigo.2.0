'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Andrea Coqueco
' Created          : 16/10/2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class ExternalPatientPreparationRepository
    Inherits GenericRepository(Of ExternalPatientPreparation)
    Implements IExternalPatientPreparation, Inject

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

    Public Function GetExternalPatientPreparationById(id As String, Optional tracking As Boolean = True) As ExternalPatientPreparation Implements IExternalPatientPreparation.GetExternalPatientPreparationById
        Dim res = (From bg In _context.ExternalPatientPreparation Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ExternalPatientPreparation.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New ExternalPatientPreparation
        End If
    End Function

End Class