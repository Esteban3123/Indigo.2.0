#Region "Importar"
Imports Domain.Base
#End Region

Public Interface IBankConciliationConceptsRepository

    Inherits IRepository(Of BankConciliationConcepts)

    ''' <summary>
    ''' Función que obtiene todos los conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllBankConciliationConcepts() As List(Of BankConciliationConcepts)

    ''' <summary>
    ''' Función que obtiene por codigo un concepto de conciliación bancaria
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>BankConciliationConcepts</returns>
    ''' <remarks></remarks>
    Function GetBankConciliationConceptsByCode(Code As String, Optional desatach As Boolean = True) As BankConciliationConcepts

End Interface
