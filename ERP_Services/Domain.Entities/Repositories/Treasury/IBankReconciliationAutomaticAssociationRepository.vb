#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IBankReconciliationAutomaticAssociationRepository
    Inherits IRepository(Of BankReconciliationAutomaticAssociation)

    ''' <summary>
    ''' Función que obtiene una lista de consecutivos.
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListBankReconciliationAutomaticAssociation() As List(Of BankReconciliationAutomaticAssociation)

End Interface
