'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 13-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddProductPackage
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un listado si esta en modo  agregar, cuando esta en modo edicion retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListPackageDetail As Domain.Entities.TrackableCollection(Of PackageDetail)

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion, cuando esta en modo agregar retorna nulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemPackageDetail As PackageDetail
End Class
