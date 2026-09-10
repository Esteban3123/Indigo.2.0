'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities


''' <summary>
''' Interfaz Servicio de Dominio Responsables
''' </summary>
Public Interface IGlosasResponsiblesService

    ''' <summary>
    ''' Listar todos los movimientos por responsables
    ''' </summary>
    Function listAllResponsiblesTransfer(IdResponsible As Integer) As List(Of ResponsibleMovements)

End Interface
