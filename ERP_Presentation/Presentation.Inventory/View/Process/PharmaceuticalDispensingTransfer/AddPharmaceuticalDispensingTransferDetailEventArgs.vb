#Region "Imports"

Imports Domain.Entities

#End Region

Public Class AddPharmaceuticalDispensingTransferDetailEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListPharmaceuticalDispensingTransferDetail As List(Of PharmaceuticalDispensingTransferDetail)

End Class
