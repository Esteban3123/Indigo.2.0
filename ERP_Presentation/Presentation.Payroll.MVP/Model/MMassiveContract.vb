Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class MMassiveContract
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

    Public Function ValidateMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContract_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveContract(pData, Indigo)
    End Function

    Public Function GetMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContract_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMassiveContract(pData, Indigo)
    End Function

    Public Sub SaveMassiveContract(pMassiveManualConcepts As List(Of ImportFileRow))
        IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveMassiveContract(pMassiveManualConcepts, Indigo)
    End Sub

    Public Function ValidateMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContractExtension_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveContractExtension(pData, Indigo)
    End Function

    Public Function GetMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContractExtension_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMassiveContractExtension(pData, Indigo)
    End Function

    Public Sub SaveMassiveContractExtension(pMassiveManualConcepts As List(Of ImportFileRow))
        IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveMassiveContractExtension(pMassiveManualConcepts, Indigo)
    End Sub

    Public Function ValidateMassiveDependentRelatives(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveDependentRelatives_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveDependentRelatives(pData, Indigo)
    End Function

    Public Function ValidateMassiveExternalEntities(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveExternalEntities_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveExternalEntities(pData, Indigo)
    End Function

    Public Function ValidateMassiveNovelties(pData As GenericListNovelty) As List(Of SP_ValidateMassiveNovelties_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveNovelties(pData, Indigo)
    End Function

    Public Function ValidateMassiveEmployeeSchedule(pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String) As List(Of SP_ValidateMassiveEmployeeSchedule_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateMassiveEmployeeSchedule(pData, Action, Code, Description, Status, Id, Prefix, Indigo)
    End Function

    Public Function GetEmployeeScheduleC(Code As String) As Task(Of EmployeeScheduleC)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeScheduleCAsync(Code, Indigo)
    End Function

    Public Function DeleteEmployeeScheduleDetail(IdEmployeeScheduleDetail As Integer, pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String) As Task(Of ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEmployeeScheduleDetailAsync(IdEmployeeScheduleDetail, Action, Code, Description, Status, Id, Prefix, Indigo)
    End Function

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


End Class
