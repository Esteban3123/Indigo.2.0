'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 26-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.PayrollRepository
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
#End Region



Public Class HumanTalentParameterizationAdminService
    Implements IHumanTalentParameterizationAdminService

#Region "Fields"
    Private _humanTalentParameterizationRepository As IHumanTalentParameterizationRepository
    Public Const FORM_NAME As String = "FrmHumanTalentParameterization"
#End Region


    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="humanTalentParameterizationRepository"></param>
    Public Sub New(ByVal humanTalentParameterizationRepository As IHumanTalentParameterizationRepository)

        If humanTalentParameterizationRepository Is Nothing Then
            Throw New ArgumentNullException("humanTalentParameterizationRepository Vacio")
        End If

        _humanTalentParameterizationRepository = humanTalentParameterizationRepository

    End Sub

    ''' <summary>
    ''' Función que actualiza la entidad HumanTalentParameterization
    ''' </summary>
    ''' <param name="HumanTalentParameterization"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveHumanTalentParameterization(HumanTalentParameterization As HumanTalentParameterization, audit As AuditMessage) Implements IHumanTalentParameterizationAdminService.SaveHumanTalentParameterization
        If HumanTalentParameterization Is Nothing Then
            Throw New ArgumentNullException("No existe parametrización")
        End If
        Dim UnitOfWork As IUnitWork = _humanTalentParameterizationRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of HumanTalentParameterization)
            Dim status As Integer
            If HumanTalentParameterization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                HumanTalentParameterization.CreationUser = audit.CodeUser
                HumanTalentParameterization.ModificationUser = audit.CodeUser
                HumanTalentParameterization.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            Else
                HumanTalentParameterization.CreationUser = audit.CodeUser
                HumanTalentParameterization.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            'Valido si se va a guardar o a eliminar
            _humanTalentParameterizationRepository.SaveEntity(HumanTalentParameterization)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of HumanTalentParameterization)(HumanTalentParameterization, audit, status)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función para obtener la parametrización de talento humano
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHumanTalentParameterization() As HumanTalentParameterization Implements IHumanTalentParameterizationAdminService.GetHumanTalentParameterization
        Try
            Return _humanTalentParameterizationRepository.GetHumanTalentParameterization()
        Catch ex As Exception
            Infrastructure.CrossCutting.Exceptions.IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
