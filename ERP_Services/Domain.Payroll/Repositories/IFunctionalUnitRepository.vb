Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IFunctionalUnitRepository
    Inherits IRepository(Of FunctionalUnit)

    ''' <summary>
    ''' Lista todos las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllFunctionalUnit() As List(Of FunctionalUnit)

    ''' <summary>
    ''' Obtiene una unidad funcional especifica
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns>Unidad Funcional</returns>
    ''' <remarks></remarks>
    Function GetFunctionalUnit(ByVal code As String, Optional tracking As Boolean = True) As FunctionalUnit

    ''' <summary>
    ''' Obtiene una unidad funcional especifica
    ''' </summary>
    ''' <param name="id">Codigo de la unidad funcional</param>
    ''' <returns>Unidad Funcional</returns>
    ''' <remarks></remarks>
    Function GetFunctionalUnitById(ByVal id As String, Optional tracking As Boolean = True) As FunctionalUnit

End Interface
