#Region "Imports"

Imports Application.Maintenance
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Entities
Imports Microsoft.Practices.Unity
#End Region

Partial Class MaintanceService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String, session As SessionValues) As MaintenanceSequence Implements IMaintenanceSequense.GetSequenseByIdForm
        Using sequense As IMaintenanceSequenseAdminService = Container.Current.Resolve(Of IMaintenanceSequenseAdminService)()
            Return sequense.GetSequenseByIdForm(idForm)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer, session As SessionValues) As List(Of String) Implements IMaintenanceSequense.GetNumericSequenseGroupById
        Using sequense As IMaintenanceSequenseAdminService = Container.Current.Resolve(Of IMaintenanceSequenseAdminService)()
            Return sequense.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.MaintenanceSequence, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IMaintenanceSequense.SaveSequence
        Using sequense As IMaintenanceSequenseAdminService = Container.Current.Resolve(Of IMaintenanceSequenseAdminService)()
            Return sequense.SaveSequence(seq)
        End Using
    End Function

End Class
