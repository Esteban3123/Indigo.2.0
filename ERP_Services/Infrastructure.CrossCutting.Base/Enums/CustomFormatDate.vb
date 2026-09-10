''' <summary>
''' Enumeracion para lso formatos de fechas mas comunes
''' </summary>
Public Enum CustomFormatDate As Integer
    ''' <summary>
    ''' Formato Fecha (09/06/2022) - dd/MM/yyyy
    ''' </summary>
    dd_MM_yyyy = 0
    ''' <summary>
    ''' Formato Fecha (06/09/2022) - MM/dd/yyyy
    ''' </summary>
    MM_dd_yyyy = 1
    ''' <summary>
    ''' Formato Fecha (2022/09/06) - yyyy/MM/dd
    ''' </summary>
    yyyy_MM_dd = 2
    ''' <summary>
    ''' Formato Fecha (30/jun/2022) - dd/MMM/yyyy
    ''' </summary>
    dd_MMM_yyyy = 3
    ''' <summary>
    ''' Formato Fecha (23 de abril de 2011 06:31:22 p.m.)
    ''' </summary>
    dd_de_MMMM_de_yyyy = 4
End Enum