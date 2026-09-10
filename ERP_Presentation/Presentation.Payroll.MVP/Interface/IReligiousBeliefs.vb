Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo

Public Interface IReligiousBeliefs
    Inherits IcrudBase

    Property Code As String

    Property Description As String

    Property Status As Boolean

    ReadOnly Property MyTag As Object

    WriteOnly Property ActionsOnControls As Boolean

    Property Sequense As Domain.Entities.PayrollSequence

End Interface
