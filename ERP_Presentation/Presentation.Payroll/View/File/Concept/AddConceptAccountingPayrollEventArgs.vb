Imports Domain.Payroll.Entities

Public Class AddConceptAccountingPayrollEventArgs

    Inherits EventArgs

    Property ListConceptAccountingStructure As List(Of ConceptAccountingStructure)

    Property ConceptAccountingStructure As ConceptAccountingStructure

    Property EditMode As Boolean

    Property ImportDataMode As Boolean

End Class
