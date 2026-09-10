using Domain.Entities;
using System.Collections.Generic;

/// <summary>
/// Clase que contiene el resultado del proceso de búsqueda de coincidencias 
/// en la conciliación bancaria automática.
/// </summary>
public class BankReconciliationCoincidencesResult
{
    /// <summary>
    /// Lista de documentos con la propiedad Reconciled actualizada según las coincidencias encontradas.
    /// </summary>
    public List<BankReconciliationAutomaticDetail> Documents { get; set; }

    /// <summary>
    /// Lista de extractos con la propiedad Reconciled actualizada según las coincidencias encontradas.
    /// </summary>
    public List<BankReconciliationAutomaticExtractDetail> Extracts { get; set; }

    /// <summary>
    /// Lista de asociaciones creadas durante el proceso de conciliación.
    /// </summary>
    public List<BankReconciliationAutomaticAssociation> Associations { get; set; }

    /// <summary>
    /// Constructor por defecto.
    /// </summary>
    public BankReconciliationCoincidencesResult()
    {
        Documents = new List<BankReconciliationAutomaticDetail>();
        Extracts = new List<BankReconciliationAutomaticExtractDetail>();
        Associations = new List<BankReconciliationAutomaticAssociation>();
    }
}

