Imports Domain.Base.Entities

Public Interface IInteropCostServices
    Inherits IDisposable
    Function ValidateDistributionManpowerSave(ByVal distributionManpower As DistributionManpower) As ActionResult
    Function ValidateDistributionFixedAssetSave(ByVal distributionFixedAsset As DistributionFixedAsset) As ActionResult
    Function ValidateDistributionIntermediateSave(ByVal distributionIntermediate As DistributionIntermediate) As ActionResult
    Function ValidateDistributionSecondarySave(ByVal distributionIntermediate As DistributionSecondary) As ActionResult

    Function ValidateProductionCenterSave(productionCenter As ProductionCenter) As ActionResult

End Interface
