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
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Public Function ListAllFileContainersForm(session As SessionValues) As List(Of Domain.DocumentalSystem.Entities.FileContainersForm) Implements IFileContainersForm.ListAllFileContainersForm
        Dim FileContainersForm As IFileContainersFormAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainersFormAdminService)()
        Return FileContainersForm.ListAllFileContainersForm(session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Archivador
    ''' </summary>
    ''' <param name="Id">Id Contenedor de archivos</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Public Function ListFileContainersFormByIdFileContainer(Id As String, session As SessionValues) As List(Of Domain.DocumentalSystem.Entities.FileContainersForm) Implements IFileContainersForm.ListFileContainersFormByIdFileContainer
        Dim FileContainersForm As IFileContainersFormAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainersFormAdminService)()
        Return FileContainersForm.ListFileContainersFormByIdFileContainer(Id, session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Formulario
    ''' </summary>
    ''' <param name="Id">Id Formulario</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    Public Function ListFileContainersFormByIdForm(Id As String, session As SessionValues) As List(Of Domain.DocumentalSystem.Entities.FileContainersForm) Implements IFileContainersForm.ListFileContainersFormByIdForm
        Dim FileContainersForm As IFileContainersFormAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IFileContainersFormAdminService)()
        Return FileContainersForm.ListFileContainersFormByIdForm(Id, session.AuditMessageWcf)
    End Function


End Class
