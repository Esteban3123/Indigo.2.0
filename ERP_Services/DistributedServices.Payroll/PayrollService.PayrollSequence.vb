#Region "Imports"

Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class PayrollService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String, session As SessionValues) As Domain.Entities.PayrollSequence Implements IPayrollSequence.GetSequenseByIdForm
        Using sequenseAdminService As IPayrollSequenceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSequenceAdminService)()
            Return sequenseAdminService.GetSequenseByIdForm(idForm)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer, session As SessionValues) As List(Of String) Implements IPayrollSequence.GetNumericSequenseGroupById
        Using sequenseAdminService As IPayrollSequenceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSequenceAdminService)()
            Return sequenseAdminService.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.PayrollSequence, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IPayrollSequence.SaveSequence
        Using sequenseAdminService As IPayrollSequenceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSequenceAdminService)()
            Return sequenseAdminService.SaveSequence(seq)
        End Using
    End Function

End Class
