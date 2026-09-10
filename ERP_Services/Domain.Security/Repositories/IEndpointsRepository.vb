Imports Domain.Security.Entities

Public Interface IEndpointsRepository

    Function GetEndpointsByContainer(idContainer As Int32) As List(Of Endpoints)
    Function GetEndpointsByContainerCode(CodeContainer As String, CodeEndPoint As String) As Endpoints

End Interface