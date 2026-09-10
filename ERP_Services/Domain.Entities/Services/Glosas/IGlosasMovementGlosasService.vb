'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities


''' <summary>
''' Interfaz Servicio de Dominio Movimientos Glosas
''' </summary>
Public Interface IGlosasMovementGlosasService

    ''' <summary>
    ''' Función para transferir responsables en los movimientos
    ''' de glosas
    ''' </summary>
    Function TransferResponsibleMovements(ListResponsiblesMovements As List(Of ResponsibleMovements), opt As Integer) As ActionResult(Of List(Of GlosaMovementGlosa))

End Interface
