Imports Domain.Base.Entities

Public Interface ICostServices
    Inherits IDisposable

    Function ValidateDistributionIntermediateSave(ByVal distributionIntermediate As CostDistributionIntermediate) As ActionResult

#Region "AverageStandardCost"
    Function ImportOrCopyAndPasteStandarCostDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails))
#End Region

End Interface
