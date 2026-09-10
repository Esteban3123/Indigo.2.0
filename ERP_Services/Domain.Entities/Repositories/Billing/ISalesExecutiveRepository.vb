Imports Domain.Base

Public Interface ISalesExecutiveRepository
    Inherits IRepository(Of SalesExecutive)

    ''' <summary>
    ''' obtiene el ejecutivo de ventas por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSalesExecutiveById(id As Integer) As SalesExecutive

    ''' <summary>
    ''' obtiene el ejecutivo de ventas por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSalesExecutiveByCode(Code As String) As SalesExecutive


End Interface
