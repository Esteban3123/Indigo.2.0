Imports Presentation.Base

Public Interface IBatchSerialSetting
    Inherits ICrudBase
#Region "Properties"
    ''' <summary>
    ''' propiedad que activa el codigo por central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property ActivateMixingStation As Boolean

    ''' <summary>
    ''' establece el tipo de codigo a usar de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property CodeMSType As Boolean?

    ''' <summary>
    ''' propiedad que activa el codigo por tipo de de dosis unitaria
    ''' </summary>
    ''' <returns></returns>
    Property ActivateUnitDoseType As Boolean

    ''' <summary>
    ''' establece el tipo de codigo a usar del tipo de dosis unitaria
    ''' </summary>
    ''' <returns></returns>
    Property CodeUDTType As Boolean?

    ''' <summary>
    ''' propiedad que activa el uso de la fecha en la creacion del lote
    ''' </summary>
    ''' <returns></returns>
    Property ActivateDateFormatType As Boolean

    ''' <summary>
    ''' propiedad que establece el tipo del formato de fecha a emplear
    ''' </summary>
    ''' <returns></returns>
    Property DateFormatType As Byte?

    ''' <summary>
    ''' establece la relacion de la secuencia numerica a escoger
    ''' </summary>
    ''' <returns></returns>
    Property SequenseId As Integer?
#End Region


    Sub DeleteBlockedRecord()
    Sub CleanControls()
    Function LoadControls() As Task

End Interface
