'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Juan Carlos Bermudez
' Created          : 29-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
#End Region

Public Class PortfolioReclassificationAdminService
    Implements IPortfolioReclassificationAdminService


#Region "Fields"
    Private _portfolioReclassificationRepository As IReclassificationRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="portfolioReclassificationRepository">Repositorio de la entidad reclasificacion de documento</param>
    Public Sub New(ByVal portfolioReclassificationRepository As IReclassificationRepository)
        If (portfolioReclassificationRepository Is Nothing) Then
            Throw New ArgumentNullException("portfolioNoteConceptRepository Vacio")
        End If
        _portfolioReclassificationRepository = portfolioReclassificationRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una reclasificación de documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReclassificationByCode(code As String, audit As AuditMessage) As PortfolioReclassification Implements IPortfolioReclassificationAdminService.GetReclassificationByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Dim portfolioReclassification = _portfolioReclassificationRepository.GetReclassification(code)
            If portfolioReclassification IsNot Nothing AndAlso portfolioReclassification.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioReclassification)(portfolioReclassification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioReclassification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioReclassification()
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _portfolioReclassificationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
