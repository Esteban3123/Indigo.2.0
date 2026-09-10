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
    ''' Funcion para obtener metadata según archivador
    ''' </summary>
    ''' <param name="sessionInf">Objeto sessionInfo</param>
    ''' <returns>Lista de Metadata</returns>
    Public Function GetMetadataByFileContainer(sessionInf As SessionInfo) As List(Of Domain.DocumentalSystem.Entities.Metadata) Implements IMetaData.GetMetadataByFileContainer
        Dim MetaData As IMetaDataAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IMetaDataAdminService)()
        Return MetaData.GetMetadataByFileContainer(sessionInf.aux)
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de metadata.
    ''' </summary>
    ''' <returns>Lista de Metadata</returns>
    Public Function ListAllMetadata(sessionInf As SessionInfo) As List(Of Domain.DocumentalSystem.Entities.Metadata) Implements IMetaData.ListAllMetadata
        Dim MetaData As IMetaDataAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IMetaDataAdminService)()
        Return MetaData.ListAllMetadata()
    End Function

    ''' <summary>
    ''' Funcion Lista de Metadata 
    ''' </summary>
    ''' <param name="IdFileContainer">Id del contenedor</param>
    ''' <param name="Indigo">Variable de sesion</param>
    ''' <returns>Lista de Metada</returns>
    ''' <remarks></remarks>
    Public Function ListMetadataByFileContainer(ByVal IdFileContainer As String, ByVal Indigo As SessionValues) As List(Of Domain.DocumentalSystem.Entities.Metadata) Implements IMetaData.ListMetadataByFileContainer
        Dim MetaData As IMetaDataAdminService = IocFactory.Instance(Indigo.DocumentalContainer).CurrentContainer.Resolve(Of IMetaDataAdminService)()
        Return MetaData.GetMetadataByFileContainer(IdFileContainer)
    End Function
    ''' <summary>
    ''' Funcion para eliminar metadata
    ''' </summary>
    ''' <param name="_objMetadata"></param>
    Public Function DeleteMetadata(_objMetadata As Domain.DocumentalSystem.Entities.Metadata, indigo As SessionValues) As Domain.Base.Entities.ActionResult Implements IMetaData.DeleteMetadata
        Dim MetaData As IMetaDataAdminService = IocFactory.Instance(indigo.DocumentalContainer).CurrentContainer.Resolve(Of IMetaDataAdminService)()
        Return MetaData.DeleteMetadata(_objMetadata)
    End Function
    ''' <summary>
    ''' Funcion para guardar metadata
    ''' </summary>
    ''' <param name="_listMetadata"></param>
    Public Function saveListMetata(_listMetadata As List(Of Domain.DocumentalSystem.Entities.Metadata), indigo As SessionValues) As Domain.Base.Entities.ActionResult Implements IMetaData.saveListMetata
        Dim MetaData As IMetaDataAdminService = IocFactory.Instance(indigo.DocumentalContainer).CurrentContainer.Resolve(Of IMetaDataAdminService)()
        Return MetaData.saveListMetata(_listMetadata)
    End Function

End Class
