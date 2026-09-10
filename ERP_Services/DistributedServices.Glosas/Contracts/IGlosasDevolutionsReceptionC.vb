Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasDevolutionsReceptionC

#Region "DevolutionsReceptionC"

    ''' <summary>
    ''' Funcion para listar todas las cabeceras de devoluciones.
    ''' </summary>
    ''' <returns>Lista de cabeceras devoluciones</returns>
    <OperationContract>
    Function ListAllDevolutionC(ByVal session As SessionValues) As List(Of GlosaDevolutionsReceptionC)

    ''' <summary>
    ''' Elimina una cabecera de devolución.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Devolución</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteDevolutionC(ByVal ConciliacionC As GlosaDevolutionsReceptionC, ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda una cabecera de devolución.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Devolución</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveDevolutionC(ByVal ConciliacionC As GlosaDevolutionsReceptionC, listConciliationD As List(Of GlosaDevolutionsReceptionD), ByVal session As SessionValues) As ActionResult(Of GlosaDevolutionsReceptionC)
    ''' <summary>
    ''' Obtiene una cabecera devolución por código.
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    <OperationContract>
    Function GetDevolutionC(ByVal code As String, ByVal session As SessionValues) As GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Obtiene una cabecera devolución especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    <OperationContract>
    Function GetDevolutionCByConsecutive(ByVal Consecutive As String, ByVal session As SessionValues) As GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Confirma la Devolución
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución Cabecera</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    <OperationContract>
    Function ConfirmDevolutionC(DevolutionC As GlosaDevolutionsReceptionC, ByVal session As SessionValues) As ActionResult

#End Region

End Interface
