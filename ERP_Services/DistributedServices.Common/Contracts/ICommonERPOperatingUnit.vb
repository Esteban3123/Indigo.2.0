Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPOperatingUnit

    ''' <summary>
    ''' Obtiene una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetOperatingUnitByCode(code As String, session As SessionValues) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' Obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetOperatingUnitById(id As Integer, session As SessionValues) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' Guarda o actualiza una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveOperatingUnit(operatingUnit As OperatingUnit, session As SessionValues) As ActionResult(Of OperatingUnit)

    ''' <summary>
    ''' elimina una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteOperatingUnit(operatingUnit As OperatingUnit, session As SessionValues) As ActionResult


    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllOperatingUnit(session As SessionValues, transactionContainer As String) As List(Of OperatingUnit)

    ''' <summary>
    ''' Lista todas las unidades operativas en SqlCommand
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllOperatingUnitCommand(session As SessionValues, transactionContainer As String) As List(Of OperatingUnit)

End Interface
