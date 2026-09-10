'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository

#End Region

Public Class MMedicinesProduction
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function SaveMedicinesProduction(ByVal MedicinesProduction As MedicinesProduction) As Task(Of ActionResult(Of MedicinesProduction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveMedicinesProductionAsync(MedicinesProduction, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function ImportMedicinesProduction(data As List(Of List(Of String)), CMConfigurationId As Integer) As Task(Of ActionResult(Of List(Of SP_ImportMedicinesProduction_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ImportMedicinesProductionAsync(data, CMConfigurationId)
    End Function

    Public Async Function DeleteMedicinesProduction(listIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteMedicinesProductionAsync(listIds, _sessionValues.TransactionalContainer, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetMedicineProductionByID(id As Integer) As Task(Of MedicinesProduction)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetMedicineProductionByIDAsync(id)
    End Function

    Public Function GetProductionLinesByCMCenterAttention(Codecenter As String) As String
        Dim filtroConsulta As String = "CodeCenterAttention = " & Codecenter
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of ViewListCMCenterAttentionXpo)(filtroConsulta).ProductionLineIds
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
