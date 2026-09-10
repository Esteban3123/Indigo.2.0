Imports Domain.Entities
Public Class DashBoardPharmacyEventArgs
    Inherits EventArgs

    Property PharmaceuticalDispensing As PharmaceuticalDispensing

    Property PharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution

    Property StatusTransactionVie As Boolean

    Property StatusTransactionHeon As Boolean

    ''' <summary>
    ''' Resultado de la integración entre Medilaser y FarmaQx
    ''' </summary>
    ''' <returns></returns>
    Property SP_SaveDispensingByPatientMedilaser_Result As SP_SaveDispensingByPatientMedilaser_Result

End Class
