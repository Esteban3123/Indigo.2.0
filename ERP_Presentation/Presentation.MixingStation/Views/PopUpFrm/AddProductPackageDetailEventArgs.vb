'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Judy Andrea Díaz Reyes
' Created          : 04-07-2019
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddProductPackageDetailEventArgs
	Inherits EventArgs

	''' <summary>
	''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Property ListPackageDetail As List(Of PackageDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemPackageDetail As PackageDetail

    ''' <summary>
    ''' Retorna un listado de item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemsPackageDetailAntibiotic As New List(Of PackageDetail)

    ''' <summary>
    ''' Obtiene o establece si el item esta en modo edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ImportDataMode As Boolean


    Property FormulationType As Integer


    Property VolumenTotalVehicle As Decimal

    Property ConcentrationWeightVolumen As Decimal

    ''' <summary>
    ''' Retorna un listado de detalles modificados (por ajuste de medicamento complementario)
    ''' </summary>
    Property ModifiedPackageDetails As List(Of PackageDetail)

    ''' <summary>
    ''' Indica si se agregó un medicamento complementario y se ajustó el principal
    ''' </summary>
    Property ComplementaryMedicineAdded As Boolean

End Class
