Imports Domain.Entities
Imports Presentation.Base

Public Interface IAverageStandardCost
    Inherits ICrudBase

#Region "Fields"
    Property Code As String
    Property Name As String
    Property StartDate As DateTime
    Property EndDate As DateTime
    Property Status As Boolean

    Property costSequence As CostSecuence
    ReadOnly Property tagForm As String
#End Region
End Interface
