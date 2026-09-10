'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class ConceptAccountingStructureAdminService
    Implements IConceptAccountingStructureAdminService

    Private _ConceptAccountingStructureRepository As IConceptAccountingStructureRepository

    Public Sub New(ByVal ConceptAccountingStructure As IConceptAccountingStructureRepository)
        If ConceptAccountingStructure Is Nothing Then
            Throw New ArgumentNullException("ConceptAccountingStructure Vacio")
        End If
        _ConceptAccountingStructureRepository = ConceptAccountingStructure
    End Sub

    Public Function GetAccountingStructureByInterfaceConceptId(InterfaceName As String, ConceptId As Integer) As List(Of ConceptAccountingStructure) Implements IConceptAccountingStructureAdminService.GetAccountingStructureByInterfaceConceptId
        If String.IsNullOrEmpty(InterfaceName) Then
            Throw New ArgumentNullException("InterfaceName Vacio")
        End If
        If String.IsNullOrEmpty(ConceptId) Then
            Throw New ArgumentNullException("ConceptId Vacio")
        End If
        Try
            Return _ConceptAccountingStructureRepository.GetAccountingStructureByInterfaceConceptId(InterfaceName, ConceptId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ConceptAccountingStructure)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ConceptAccountingStructureRepository = Nothing
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
