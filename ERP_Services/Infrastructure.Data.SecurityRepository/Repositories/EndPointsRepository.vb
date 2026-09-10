Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.Data.Base

Public Class EndPointsRepository
    Inherits BaseRepository
    Implements IEndpointsRepository
    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="GroupRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)

        _context = contex
    End Sub


    Public Function GetEndpointsByContainer(idContainer As Integer) As List(Of Endpoints) Implements IEndpointsRepository.GetEndpointsByContainer
        Dim params As List(Of (String, Object)) = New List(Of (String, Object))
        params.Add(("@idContainer", idContainer))

        Dim query = "SELECT Id ,Code ,IdContainer ,UrlBase FROM Security.Endpoints where IdContainer = @idContainer"

        Return Me.ExecuteQueryDR(Of Endpoints)(query, params)
    End Function

    ''' <summary>
    ''' funcion que lista el enpoint por codigo y base de datos
    ''' </summary>
    ''' <param name="CodeContainer"></param>
    ''' <param name="CodeEndPoint"></param>
    ''' <returns></returns>
    Public Function GetEndpointsByContainerCode(CodeContainer As String, CodeEndPoint As String) As Endpoints Implements IEndpointsRepository.GetEndpointsByContainerCode
        Dim params As List(Of (String, Object)) = New List(Of (String, Object))
        params.Add(("@CodeContainer", CodeContainer))
        params.Add(("@CodeEndPoint", CodeEndPoint))

        Dim query = "SELECT ep.Id ,ep.Code ,ep.IdContainer ,ep.UrlBase 
                        FROM Security.Endpoints ep with(Nolock)
                        JOIN Security.Containers c with(Nolock) ON ep.IdContainer=c.Id
                        where c.TransactionalContainer = @CodeContainer and ep.Code =@CodeEndPoint "

        Return Me.ExecuteQueryDR(Of Endpoints)(query, params).FirstOrDefault()
    End Function
End Class