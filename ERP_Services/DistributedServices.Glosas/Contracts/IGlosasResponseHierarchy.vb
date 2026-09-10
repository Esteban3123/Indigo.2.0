Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasResponseHierarchy
    ''' <summary>
    ''' Lists the GlosasResponseHierarchy
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListResponseHierarchy(session As SessionValues) As List(Of GlosasResponseHierarchy)
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="Code">el codigo del Responsible</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetResponseHierarchy(ByVal Code As String, session As SessionValues) As GlosasResponseHierarchy
    ''' <summary>
    ''' consulta una jerarquía de respuesta especifico
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetResponseHierarchyById(ByVal id As Integer, session As SessionValues) As GlosasResponseHierarchy
    ''' <summary>
    ''' Elimina una jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy">el Grupo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteGlosasResponseHierarchy(ByVal GlosasResponseHierarchy As GlosasResponseHierarchy, session As SessionValues) As ActionResult
    ''' <summary>
    ''' graba una Jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy">el Responsiblee</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGlosasResponseHierarchy(ByVal GlosasResponseHierarchy As GlosasResponseHierarchy, session As SessionValues) As ActionResult(Of GlosasResponseHierarchy)
End Interface
