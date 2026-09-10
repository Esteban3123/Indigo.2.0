'***********************************************************************
' Assembly         : DistributedService.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.DocumentalSystem.Entities
Imports Domain.DocumentalSystem
Imports Infrastructure.CrossCutting.IOC
Imports Application.DocumentalSystem
Imports Infrastructure.CrossCutting.Base
#End Region
Partial Class DocumentalSystemService

    ''' <summary>
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="session">objeto SessionInf</param>
    ''' <returns>Objeto Archivador</returns>
    Public Function GetFileContainer(Id As String, session As SessionValues) As Domain.DocumentalSystem.Entities.FileContainer Implements IFileContainer.GetFileContainer
        Dim FileContainer As IFileContainerAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainerAdminService)()
        Return FileContainer.GetFileContainer(Id, session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <param name="session">objeto SessionInf</param>
    ''' <returns>Lista de Archivadores</returns>
    Public Function ListAllFileContainers(session As SessionValues) As List(Of Domain.DocumentalSystem.Entities.FileContainer) Implements IFileContainer.ListAllFileContainers
        Dim FileContainer As IFileContainerAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainerAdminService)()
        Return FileContainer.ListAllFileContainers(session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función para borrar un archivador
    ''' </summary>
    ''' <param name="fileContainerAux">Objeto Archivador</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteFileContainer(fileContainerAux As Domain.DocumentalSystem.Entities.FileContainer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.DocumentalSystem.Entities.FileContainer) Implements IFileContainer.DeleteFileContainer
        Dim FileContainer As IFileContainerAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainerAdminService)()
        Return FileContainer.DeleteFileContainer(fileContainerAux, session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función para guardar un archivador
    ''' </summary>
    ''' <param name="fileContainerAux">Objeto Archivador</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveFileContainer(fileContainerAux As FileContainer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.DocumentalSystem.Entities.FileContainer) Implements IFileContainer.SaveFileContainer
        Dim FileContainer As IFileContainerAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainerAdminService)()
        Return FileContainer.SaveFileContainer(fileContainerAux, session.AuditMessageWcf)
    End Function

End Class
