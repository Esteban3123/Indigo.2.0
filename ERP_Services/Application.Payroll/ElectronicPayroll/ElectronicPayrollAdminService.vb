#Region "Imports"

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ElectronicPayrollAdminService
    Implements IElectronicPayrollAdminService

#Region "Fields"

    Private _electronicPayrollRepository As IElectronicPayrollRepository
    Private _secuenseCRepository As Domain.Entities.IPayrollSequenceRepository
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    Private _settingsAccountRepository As Domain.Entities.ISettingsAccountRepository


#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(electronicPayrollRepository As IElectronicPayrollRepository,
                   secuenseCRepository As Domain.Entities.IPayrollSequenceRepository,
                   secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository,
                   settingsAccountRepository As Domain.Entities.ISettingsAccountRepository)
        _electronicPayrollRepository = electronicPayrollRepository
        _secuenseCRepository = secuenseCRepository
        _secuenseDRepository = secuenseDRepository
        _settingsAccountRepository = settingsAccountRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera una nota de ajuste a partir de un documento electrónico de nómina
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function GenerateAdjustmentNote(operatingUnitId As Integer, electronicPayroll As ElectronicPayroll, session As SessionValues) As ActionResult(Of ElectronicPayroll) Implements IElectronicPayrollAdminService.GenerateAdjustmentNote
        Try
            Dim PayrollSequenseDetailId As Integer = 0
            Dim payrollSequense = _secuenseCRepository.GetSequenseByIdForm("2636")
            If payrollSequense.Id = 0 Then
                Return New ActionResult(Of ElectronicPayroll) With {.StateResult = False, .Message = "No existe secuencia numérica para las notas de ajuste de soportes de pago de nómina electrónica."}
            ElseIf payrollSequense.IsManual Then
                Return New ActionResult(Of ElectronicPayroll) With {.StateResult = False, .Message = "La secuencia numerica de las notas de ajuste de soportes de pago de nómina electrónica no puede ser manual."}
            End If

            If payrollSequense.Scope = "O" Then 'Si la secuencia es por organización
                PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Select x.Id).FirstOrDefault()
            Else 'Si la secuencia es por unidad operativa
                If (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = operatingUnitId Select x).Count = 0 Then
                    Return New ActionResult(Of ElectronicPayroll) With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de las notas de ajuste de soportes de pago de nómina electrónica."}
                End If
                PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = operatingUnitId Select x.Id).FirstOrDefault()
            End If

            Dim adjustmentNoteElectronicPayroll = New ElectronicPayroll With {
                                .DocumentType = 2,
                                .Year = electronicPayroll.Year,
                                .Month = electronicPayroll.Month,
                                .EmployeePartyId = electronicPayroll.EmployeePartyId,
                                .EntityName = electronicPayroll.GetType().Name,
                                .EntityId = electronicPayroll.Id,
                                .CUNE = String.Empty,
                                .Status = 1,
                                .CreationDate = DateTime.Now
                            }

            Dim seq = Me._secuenseDRepository.GetSequenseDById(PayrollSequenseDetailId)
            If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                adjustmentNoteElectronicPayroll.Prefix = seq.Sequense.Pattern.Replace("#", "")
                adjustmentNoteElectronicPayroll.DocumentNumber = seq.Next

                seq.Next += 1
                Me._secuenseDRepository.SaveEntity(seq)
                Me._secuenseDRepository.UnitWork.Commit()
            Else
                Return New ActionResult(Of ElectronicPayroll) With {.StateResult = False, .Message = "_Seq01_"}
            End If

            adjustmentNoteElectronicPayroll.FilePath = System.IO.Path.Combine(
                                Utils.GetPathElectronicDocuments(),
                                session.TransactionalContainer,
                                adjustmentNoteElectronicPayroll.Year,
                                adjustmentNoteElectronicPayroll.Month,
                                adjustmentNoteElectronicPayroll.getDocumentTypeName(),
                                adjustmentNoteElectronicPayroll.Prefix,
                                adjustmentNoteElectronicPayroll.DocumentNumber
                            )

            _electronicPayrollRepository.SaveEntity(adjustmentNoteElectronicPayroll)
            _electronicPayrollRepository.UnitWork.Commit()


            Return New ActionResult(Of ElectronicPayroll) With {.StateResult = True, .ObjectEmbbeded = adjustmentNoteElectronicPayroll, .Message = "Nota de Ajuste generada correctamente"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of ElectronicPayroll) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'Others Services
            End If

            _electronicPayrollRepository = Nothing
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
