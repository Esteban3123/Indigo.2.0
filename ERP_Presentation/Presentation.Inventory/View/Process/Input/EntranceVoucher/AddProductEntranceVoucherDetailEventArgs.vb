'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 15-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddProductEntranceVoucherDetailEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListEntranceVoucherDetail As List(Of EntranceVoucherDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemEntranceVoucherDetail As EntranceVoucherDetail

    ''' <summary>
    ''' Obtiene o establece si el item esta en modo edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

    ''' <summary>
    ''' Obtiene o establece si el item esta en modo importar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ImportDataMode As Boolean

End Class

