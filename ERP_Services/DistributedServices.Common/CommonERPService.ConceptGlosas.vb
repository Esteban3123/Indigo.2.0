'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Raffael Eduardo Patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Common
Imports Infrastructure.CrossCutting.Base

Partial Class CommonERPService

    ''' <summary>
    ''' Funcion para listar conceptos glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptGlosas(session As SessionValues) As List(Of Domain.Entities.ConceptGlosas) Implements ICommonERPConceptGlosas.ListConceptGlosas
        Using conceptGlosasAdmin As IConceptGlosasAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosasAdminService)()
            Return conceptGlosasAdmin.ListConceptGlosas()
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según tipo
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Public Function ListConceptGlosaByType(type As String, session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Entities.ConceptGlosas) Implements ICommonERPConceptGlosas.ListConceptGlosaByType
        Using conceptGlosasAdmin As IConceptGlosasAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosasAdminService)()
            Return conceptGlosasAdmin.ListConceptGlosasByType(type)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Public Function ListConceptGlosaByListTypes(types As List(Of String), session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Entities.ConceptGlosas) Implements ICommonERPConceptGlosas.ListConceptGlosaByListTypes
        Using conceptGlosasAdmin As IConceptGlosasAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosasAdminService)()
            Return conceptGlosasAdmin.ListConceptGlosaByListTypes(types)
        End Using
    End Function

End Class
