Imports Domain.Entities
Imports DevExpress.Xpo

''' <summary>
''' evento utilizado para los conceptos de egreso
''' </summary>
Public Class AddProductLevelEventArgs
    Inherits EventArgs

    Property InventoryProductLevel As InventoryProduct
    Property ConversionUnit As Long

End Class

Public Class AddProductRateEventArgs
    Inherits EventArgs

    Property ListProductRateDetail As List(Of ProductRateDetail)

    Property ProductRateDetail As ProductRateDetail

    Property ListProductRateDetailValidate As List(Of ProductRateDetail)

    Property EditMode As Boolean

End Class

Public Class AddPharmaceuticalDispensingDetailEventArgs
    Inherits EventArgs

    Property PharmaceuticalDispensingDetail As PharmaceuticalDispensingDetail

    ''' <summary>
    ''' Permite saber si el almacen que se esta agregando es virtual o no
    ''' </summary>
    ''' <returns></returns>
    Property VirtualStore As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Custody As Boolean
End Class