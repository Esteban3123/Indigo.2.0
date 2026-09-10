'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MDashboardConfirmationUnitDose
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

    Public Async Function GetConfirmationUnitDoseById(ByVal id As Integer) As Task(Of ActionResult(Of ConfirmationUnitDose))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetConfirmationUnitDoseByIdAsync(id)
    End Function

    Public Async Function SaveConfirmationUnitDose(ByVal ConfirmationUnitDose As ConfirmationUnitDose) As Task(Of ActionResult(Of ConfirmationUnitDose))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveConfirmationUnitDoseAsync(ConfirmationUnitDose, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses As List(Of ConfirmationUnitDose), package As Package) As Task(Of ActionResult(Of ConfirmationUnitDose))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveConfirmationUnitDoseAndPackageListAsync(confirmationUnitDoses, package, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Verifica una solicitud de tipo NPT
    ''' </summary>
    Public Async Function VerifyRequestNPT(confirmationUnitDoseTmp As ConfirmationUnitDoseValidations) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.VerifyRequestNPTAsync(confirmationUnitDoseTmp)
    End Function

    Public Async Function SaveConfirmationUnitDoseAndPackage(confirmationUnitDose As ConfirmationUnitDose, package As Package) As Task(Of ActionResult(Of ConfirmationUnitDose))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveConfirmationUnitDoseAndPackageAsync(confirmationUnitDose, package, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function DeleteConfirmationUnitDose(listIds As List(Of Integer), TransactionalContainer As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteConfirmationUnitDoseAsync(listIds, Me._sessionValues.AuditMessageWcf, TransactionalContainer)
    End Function

    Public Async Function UpdateMedicalOrderCM(args As Object) As Task(Of ActionResult(Of SP_UpdateMedicalOrder_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateMedicalOrderCMAsync(parameter, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function AnnulateUnitDosesAsync(data As List(Of AnnulateUnitDoseModel)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.AnnulateUnitDosesAsync(data, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SetSafeStatusAsync(items, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion de guardado asyncrona de paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <returns></returns>
    Public Async Function SavePackageAsync(ByVal package As Package) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SavePackageAsync(package, 0, Me._sessionValues.AuditMessageWcf)
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
