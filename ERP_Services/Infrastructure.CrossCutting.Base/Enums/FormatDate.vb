''' <summary>
''' Enumeracion para lso formatos de fechas mas comunes
''' </summary>
Public Enum FormatDate As Integer
    ''' <summary>
    ''' Formato Fecha Corta (23/04/2011)
    ''' </summary>
    Shortdate = 0
    ''' <summary>
    ''' Formato fecha Larga (sábado, 23 de abril de 2011)
    ''' </summary>
    Longdate = 1
    ''' <summary>
    ''' Formato FechaCorta Extendida (sábado, 23 de abril de 2011 06:31 p.m.)
    ''' </summary>
    FullShortDatetime = 2
    ''' <summary>
    ''' Formato Fecha Larga Extendida (sábado, 23 de abril de 2011 06:31:22 p.m.)
    ''' </summary>
    FullLongDatetime = 3
    ''' <summary>
    ''' Formato Fecha Corta General (23/04/2011 06:31 p.m.)
    ''' </summary>
    GeneralShortDatetime = 4
    ''' <summary>
    ''' Formato Fecha Corta General (23/04/2011 06:31:45 p.m.)
    ''' </summary>
    GeneralLongDatetime = 5

End Enum