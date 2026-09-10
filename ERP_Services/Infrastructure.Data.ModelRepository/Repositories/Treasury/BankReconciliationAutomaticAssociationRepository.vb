Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class BankReconciliationAutomaticAssociationRepository
    Inherits GenericRepository(Of BankReconciliationAutomaticAssociation)
    Implements IBankReconciliationAutomaticAssociationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener las relaciones
    ''' </summary>
    ''' <returns>Lista de Consecutivos</returns>
    Public Function ListBankReconciliationAutomaticAssociation() As List(Of BankReconciliationAutomaticAssociation) Implements IBankReconciliationAutomaticAssociationRepository.ListBankReconciliationAutomaticAssociation
        Dim Busqueda = From e In _context.BankReconciliationAutomaticAssociation
                       Select e

        Return Busqueda.ToList
    End Function


End Class
